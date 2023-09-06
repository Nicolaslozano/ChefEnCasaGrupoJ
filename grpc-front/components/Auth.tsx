import React, { useEffect, useState } from "react";
import Cookies from "js-cookie";
import Login from "@/pages/login";

const Auth = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const usuario = Cookies.get("usuario");

    if (usuario) {
      setIsAuthenticated(true);
    }
  }, []);

  if (isAuthenticated) {
    return <>{children}</>;
  } else {
    return <Login/>;
  }
};

export default Auth;
