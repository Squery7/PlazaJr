import React from "react";
import { useEffect, useState } from 'react';
import './Datos.css'
import Prestamo from "./Prestamo";

function Datos() {
    const [identificador, setIdentificador] = useState(0);
    const [sesion, setSesion] = useState(false);
    const [visibility, setVisibility] = useState(true);

    const handleVerificar = (e) => {
        if (identificador == null || identificador == "") {
            window.alert("El id ingresado es nulo")
        } else {
            fetch(`https://localhost:7148/estudiante/listar/id/${identificador}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(res => res.json())
                .then(data => {
                    if (data.error) {
                        window.alert(data.mensaje)
                    } else {
                        window.alert(data.mensaje)
                        setSesion(true)
                        setVisibility(false)
                    }
                })
                .catch(error => console.log(error))
        }
    };

    const handleActualizar = (e) => {
        setIdentificador(e.target.value);
    }

    return (
        <>
            <div className="div-datos">
                <label >Ingrese su Id:</label>
                <input type="text" onChange={handleActualizar} />
                {visibility ? <button onClick={handleVerificar}>Buscar</button> : null}
                {sesion ? <Prestamo idLector={identificador} /> : null}
            </div>
        </>
    );
}

export default Datos;