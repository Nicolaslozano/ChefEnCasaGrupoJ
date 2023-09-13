import React, { useEffect, useState } from "react";
import Cookies from "js-cookie";
import { useRouter } from "next/router";
import Login from "@/pages/login";
import { siteConfig } from "@/config/site"; // Importa siteConfig

const Auth = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const router = useRouter();

  useEffect(() => {
    const usuario = Cookies.get("usuario");

    if (usuario) {
      setIsAuthenticated(true);
    } else {
      // Verifica si la ruta actual requiere autenticación
      const currentRoute = router.pathname;
      const requiresAuth = siteConfig.navMenuItems.some(
        (item) => item.href === currentRoute
      );

      if (requiresAuth) {
        router.push("/login"); // Redirige a la página de inicio de sesión
      }
    }
  }, []);

  if (isAuthenticated) {
    return <>{children}</>;
  } else {
    return <Login />;
  }
};

export default Auth;
