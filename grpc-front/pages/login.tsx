import {useState } from "react";
import {title,} from "@/components/primitives";
import DefaultLayout from "@/layouts/default";
import { Input, Button, Spacer } from "@nextui-org/react";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [name, setName] = useState("");
  const [isRegistering, setIsRegistering] = useState(false);


  const handleLogin = async () => {
    
    try{
    const url = `https://localhost:44323/api/Usuarios?username=${encodeURIComponent(username)}&password=${encodeURIComponent(password)}`;
    const response = await fetch(url);
    const parsed = await response.json();

        if(parsed.User === username && parsed.Password === password){
            window.location.href="/user";
        }
        else{
           alert("Las credenciales son incorrectas")
        }

    }
    catch{
        alert("No se pudo conectar con el servidor");
    }
  };
  

  const handleRegister = async () => {
    const registrationData = {
      nombre: name,
      email,
      user: username,
      password,
    };
  
    try {
      
      
    const url = "https://localhost:44323/api/Usuarios";
      const response = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(registrationData),
      });
  
      if (response.ok) {
        alert("Registro exitoso");
      } else {
        alert("Error al registrar. Por favor, intenta de nuevo más tarde.");
      }
    } catch (error) {
      console.error("Error:", error);
      alert("No se pudo conectar con el servidor");
    }
  };





  return (
    <DefaultLayout>
      <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10 mt-24">
        <div className="inline-block max-w-lg text-center justify-center">
          <h1 className={title({ color: "blue" })}>
            {isRegistering ? "Registrarse" : "Iniciar Sesión"} &nbsp;
          </h1>
        </div>
        <Spacer />
      </section>
      <div className="flex justify-center items-center">
        <div className="w-full max-w-md p-4">
          <Input
            className="mb-4"
            type="text"
            placeholder="Usuario"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <Input
            className="mb-4"
            type="password"
            placeholder="Contraseña"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          {isRegistering && ( 
            <>
              <Input
                className="mb-4"
                type="email"
                placeholder="Correo electrónico"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              <Input
                className="mb-4"
                type="text"
                placeholder="Nombre"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </>
          )}
          {isRegistering ? (
            <Button color="primary" onClick={handleRegister}>
              Registrarse
            </Button>
          ) : (
            <Button color="primary" onClick={handleLogin}>
              Iniciar sesión
            </Button>
          )}
          <Spacer />
          <Button onClick={() => setIsRegistering(!isRegistering)}>
            {isRegistering ? "¿Ya tienes una cuenta?" : "¿No tienes una cuenta?"}
          </Button>
        </div>
      </div>
    </DefaultLayout>
  );
}
