export type SiteConfig = typeof siteConfig;

export const siteConfig = {
	name: "ChefEnCasa",
	description: "Recetas por el usuario para el usuario.",
	
	navMenuItems: [
		{
			label: "Profile",
			href: "/profile",
		},
		{
			label: "Login",
			href: "/login",
		  },
		{
			label: "Dashboard",
			href: "/dashboard",
		},
		{
			label: "Projects",
			href: "/projects",
		},
		{
			label: "Team",
			href: "/team",
		},
		{
			label: "Calendar",
			href: "/calendar",
		},
		{
			label: "Settings",
			href: "/settings",
		},
		{
			label: "Help & Feedback",
			href: "/help-feedback",
		},
		{
			label: "Logout",
			href: "/logout",
		},
		{
			label: "user",
			href: "/user",
		  }
	],
	links: {
		github: "https://github.com/nextui-org/nextui",
		twitter: "https://twitter.com/getnextui",
		docs: "https://nextui-docs-v2.vercel.app",
		discord: "https://discord.gg/9b6yyZKmH4",
	},
};
