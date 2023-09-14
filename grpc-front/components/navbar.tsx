import { useState } from "react";
import {
  Link,
  Navbar as NextUINavbar,
  NavbarContent,
  NavbarMenuToggle,
  NavbarBrand,
  NavbarItem,
} from "@nextui-org/react";
import NextLink from "next/link";
import Cookies from "js-cookie";

import { ThemeSwitch } from "@/components/theme-switch";
import { UserIcon } from "@/components/icons";
import { Logo } from "@/components/icons";

export const Navbar = () => {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const handleDropdownToggle = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  const handleContribucionesClick = () => {
    window.location.href = "/user";
  };

  const handleCerrarSesionClick = () => {
    // Eliminar la cookie de usuario
    Cookies.remove("usuario");
  
    // Redirigir a la página de inicio de sesión
    window.location.href = "/login";
  };

  return (
    <NextUINavbar maxWidth="xl" position="sticky">
      <NavbarContent className="basis-1/5 sm:basis-full" justify="start">
        <NavbarBrand className="gap-3 max-w-fit">
          <NextLink className="flex justify-start items-center gap-1" href="/">
            <Logo />
            <p className="font-bold text-inherit">ChefEnCasa</p>
          </NextLink>
        </NavbarBrand>
      </NavbarContent>

      <NavbarContent
        className="hidden sm:flex basis-1/5 sm:basis-full"
        justify="end"
      >
        <NavbarItem className="hidden sm:flex gap-3">
          <div className="relative">
            <div onClick={handleDropdownToggle}>
              <UserIcon className="text-default-500 cursor-pointer" />
            </div>
            {isDropdownOpen && (
              <div className="absolute right-0 mt-auto	 w-40 bg-white shadow-md p-8">
                <Link href="/user">Panel de control</Link>
                <div>
                  <Link
                    className="mt-3"
                    href="/login"
                    onClick={handleCerrarSesionClick}
                  >
                    Cerrar Sesión
                  </Link>
                </div>
              </div>
            )}
          </div>
          <ThemeSwitch />
        </NavbarItem>
      </NavbarContent>
      <NavbarContent className="sm:hidden basis-1 pl-4" justify="end">
        <ThemeSwitch />
        <NavbarMenuToggle />
      </NavbarContent>
    </NextUINavbar>
  );
};
