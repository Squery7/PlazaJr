import React from 'react';
import { useEffect, useState } from 'react';
import './Card.css';

function Card() {
    const [usuario, setUsuario] = useState("");

    const handleVerificar = (e) => {
        if (usuario == null || usuario == "") {
            window.alert("El usuario ingresado es nulo")
        } else {
            fetch(`https://localhost:7148/estudiante/listar/nombre/${usuario}`, {
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
                    }
                })
                .catch(error => console.log(error))
        }
    };

    const handleActualizar = (e) => {
        setUsuario(e.target.value);
    };

    return (
        <>
            <div className='div-card'>
                <label>Ingrese su nombre</label>
                <input type="text" onChange={handleActualizar} />
                <button onClick={handleVerificar} >Enviar</button>
            </div>
        </>
    );
};

export default Card;
