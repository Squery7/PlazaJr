import React from "react";
import { useEffect, useState } from 'react';
import { PrestamoFetch } from "../Utilities/Fetch";


function Prestamo({ idLector }) {
    const [libros, setLibros] = useState([]);
    const [selectedLibro, setSelectedLibro] = useState('');
    const [prestamos, setPrestamos] = useState([]);

    const [fecha, setFecha] = useState(""); // Fecha de devolución

    useEffect(() => {
        let isMounted = true;

        // Obtener información de libros
        fetch('https://localhost:7148/libro/listar', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(data => {
                if (isMounted) {
                    setLibros(data?.mensaje || []);
                    console.log(data?.mensaje)
                }
            })
            .catch(error => {
                console.error(error);
                setError('Hubo un error al obtener los libros.');
            });

        // Obtener información de préstamos
        fetch('https://localhost:7148/prestamo/listar', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(data => {
                if (isMounted) {
                    setPrestamos(data?.mensaje || []);
                    console.log(data?.mensaje)
                }
            })
            .catch(error => {
                console.error(error);
                setError('Hubo un error al obtener los préstamos.');
            });

        return () => {
            isMounted = false;
        };
    }, []);

    const handleLibroChange = (event) => {
        setSelectedLibro(event.target.value);
    };

    const handleFechaChange = (event) => {
        setFecha(event.target.value);
    }

    const handlePrestamo = (event) => {
        if (selectedLibro == null || selectedLibro == "") {
            window.alert("El id del libro ingresado es nulo")
        } else if (fecha == null || fecha == "") {
            window.alert("La fecha ingresada es nula")
        } else {
            let fechaActual = new Date(Date.now());
            let fechaActualString = fechaActual.toISOString().split('T')[0];

            if (fecha >= fechaActualString) {
                PrestamoFetch(idLector, selectedLibro, fecha, fechaActualString);
            } else {
                window.alert("La fecha de devolución debe ser mayor a la fecha actual")
            }
        }
    };

    return (
        <>
            <select value={selectedLibro} onChange={handleLibroChange}>
                <option value="">Selecciona un libro</option>
                {libros.map(libro => {
                    const estaPrestado = prestamos.some(prestamo => prestamo.idLibro === libro.id && prestamo.devuelto === false);
                    return (
                        <option key={libro.id} value={libro.id} disabled={estaPrestado}>
                            {libro.titulo} {estaPrestado ? '(No disponible)' : ''}
                        </option>
                    );
                })}
            </select>

            <input type="date" onChange={handleFechaChange} />
            <button onClick={handlePrestamo}>Prestar libro</button>
        </>
    );
}

export default Prestamo;