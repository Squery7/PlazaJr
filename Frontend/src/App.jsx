import { useState } from 'react'
import './App.css'
import Card from './Components/Card'
import Datos from './Components/Datos'

function App() {
  let valores = ['Busqueda por ID', 'Olvidé mi ID']
  const [id, setId] = useState(true)
  const [datos, setDatos] = useState(valores[1])

  const handleId = () => {
    if (datos === valores[0]){
      setDatos(valores[1])
    } else {
      setDatos(valores[0])
    }
    setId(!id)
  }

  return (
    <>
      <div className="main">
        {id ? <Datos /> : <Card />}
        <div className='div-datos'>
          <button onClick={handleId}>{datos}</button>
        </div>

        {/* <Datos />
        <Card /> */}
      </div>

    </>
  )
}

export default App
