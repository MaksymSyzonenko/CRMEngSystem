async function initColumnResizer(pageIdentifier) {
    const resizers = document.querySelectorAll('.resizer');
    let currentResizer;
    let startX;
    let startWidth;
    let column;
    let gridTemplateColumns;
    let columnIndex;

    const contentElement = document.querySelector('.content'); // Элемент, который нужно скрыть

    // Загрузка сохранённых размеров колонок
    async function loadColumnSizes() {
        const url = `/Settings/ColumnResizeSettings/GetColumnSizes?pageIdentifier=${pageIdentifier}`;

        try {
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${await response.text()}`);
            }

            const { columnSizes } = await response.json();
            if (columnSizes != 'none') {
                applyColumnSizes(columnSizes);
            }
            

            contentElement.classList.remove('hidden');
            contentElement.classList.add('visible');
        } catch (error) {
            contentElement.classList.remove('hidden');
            contentElement.classList.add('visible');
            console.error('Error loading column sizes:', error);
        }
    }

    function applyColumnSizes(columnSizes) {
        const parentHeader = document.querySelector('.header-part, .header-part-order');
        if (parentHeader) {
            gridTemplateColumns = columnSizes.split(' ');
            document.querySelectorAll('.header-part, .header-part-order, .card-data, .card-data-order, .result-container').forEach(part => {
                part.style.gridTemplateColumns = gridTemplateColumns.join(' ');
            });
        }
    }

    function startResizing(e, resizer) {
        currentResizer = resizer;
        startX = e.clientX;
        column = currentResizer.parentElement;
        const parentHeader = column.parentElement;
        startWidth = column.offsetWidth;

        gridTemplateColumns = window.getComputedStyle(parentHeader).gridTemplateColumns.split(' ');
        columnIndex = Array.from(parentHeader.children).indexOf(column);

        document.addEventListener('mousemove', resize);
        document.addEventListener('mouseup', stopResize);

        currentResizer.classList.add('resizing');
    }

    function resize(e) {
        if (currentResizer) {
            const newWidth = startWidth + (e.clientX - startX);
            const minWidth = currentResizer.parentElement.querySelector('span').offsetWidth;
            const totalWidth = currentResizer.parentElement.parentElement.offsetWidth;
            const percentageWidth = (newWidth / totalWidth) * 100;
            const minPercentageWidth = (minWidth / totalWidth) * 100;

            if (percentageWidth > minPercentageWidth) {
                gridTemplateColumns[columnIndex] = `${percentageWidth}%`;

                window.requestAnimationFrame(() => {
                    document.querySelectorAll('.header-part, .header-part-order, .card-data, .card-data-order, .result-container').forEach(part => {
                        part.style.gridTemplateColumns = gridTemplateColumns.join(' ');
                    });
                });
            }
        }
    }




    async function saveColumnSizesToServer(columnSizes, pageIdentifier) {
        const url = '/Settings/ColumnResizeSettings/SaveColumnSizes';

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ columnSizes, pageIdentifier })
            });

            if (!response.ok) {
                throw new Error(`Network response was not ok: ${await response.text()}`);
            }

            const result = await response.json();
            console.log('Data saved on the server:', result.message);
        } catch (error) {
            console.error('Error saving data:', error);
        }
    }

    async function stopResize() {
        if (currentResizer) {
            document.removeEventListener('mousemove', resize);
            document.removeEventListener('mouseup', stopResize);
            currentResizer.classList.remove('resizing');
            currentResizer = null;

            const columnSizes = gridTemplateColumns.join(' ');

            try {
                await saveColumnSizesToServer(columnSizes, pageIdentifier);
            } catch (error) {
                console.error('Ошибка при сохранении данных:', error);
            }
        }
    }

    // Загрузка размеров колонок при загрузке страницы
    await loadColumnSizes();

    resizers.forEach(resizer => {
        resizer.addEventListener('mousedown', function (e) {
            e.preventDefault();
            startResizing(e, resizer);
        });
    });
}
