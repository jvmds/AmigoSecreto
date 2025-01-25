export type ProductMenu = {
  name: string,
  description: string,
  path?: string,
  icon?: string,
  byProducts: ProductMenu[]
}
