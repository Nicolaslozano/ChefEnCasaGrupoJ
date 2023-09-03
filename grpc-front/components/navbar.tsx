import {
	Avatar,
	Link,
	Input,
	Navbar as NextUINavbar,
	NavbarContent,
	NavbarMenu,
	NavbarMenuToggle,
	NavbarBrand,
	NavbarItem,
	NavbarMenuItem,
} from "@nextui-org/react";

import { link as linkStyles } from "@nextui-org/theme";

import { siteConfig } from "@/config/site";
import NextLink from "next/link";
import clsx from "clsx";

import { ThemeSwitch } from "@/components/theme-switch";
import {
	SearchIcon,
	UserIcon,
} from "@/components/icons";

import { Logo } from "@/components/icons";

export const Navbar = () => {
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

			<NavbarContent className="hidden sm:flex basis-1/5 sm:basis-full" justify="end">
				<NavbarItem className="hidden sm:flex gap-3">
					<Link href={"/login"}>
						<UserIcon className="text-default-500" />
					</Link>
					<ThemeSwitch />
				</NavbarItem>
				<NavbarItem className="hidden md:flex">
				</NavbarItem>
			</NavbarContent>
			<NavbarContent className="sm:hidden basis-1 pl-4" justify="end">
				<ThemeSwitch />
				<NavbarMenuToggle />
			</NavbarContent>
		</NextUINavbar>
	);
};
