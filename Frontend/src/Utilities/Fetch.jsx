export function PrestamoFetch(idLector, selectedLibro, fecha, fechaActualString) {
    var respuestaLibros = 0;
    fetch(`https://localhost:7148/prestamo/librosDisponibles`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(res => res.json())
        .then(data => {
            if (data.error) {
                window.alert(data.mensaje);
            } else {
                respuestaLibros = data.mensaje;
                if (respuestaLibros != 0) {
                    console.log(respuestaLibros);
                    fetch(`https://localhost:7148/prestamo/guardar`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            IdLector: idLector,
                            IdLibro: selectedLibro,
                            FechaPrestamo: fechaActualString,
                            FechaDevolucion: fecha,
                            Devuelto: false
                        })
                    })
                        .then(res => res.json())
                        .then(data => {
                            if (data.error) {
                                window.alert(data.mensaje)
                            } else {
                                window.alert(data.mensaje)
                            }
                        })
                        .catch(error => console.log(error))
                } else {
                    window.alert("No hay libros disponibles.")
                }
            }
        })
        .catch(error => {
            console.error(error);
            setError('Hubo un error al obtener los libros.');
        });
}