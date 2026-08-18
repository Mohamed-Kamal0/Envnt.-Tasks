export interface NavLink{
  label: string;
  path: string;
}
export type Badge = "new" | "sale" | "none";
export type currentYear = () => number;
export const shopLinks: NavLink[] = [
  { label: "Home", path: "" },
  { label: "Shop", path: "/shop" },
  { label: "About Us", path: "/about-us" },
  { label: "Contact", path: "/contact-us" },
];
function first<T>(items: T[]): T[] | undefined {
  return items;
}
first(shopLinks);
const numbers: number[] = [1, 2, 3];
first(numbers);
