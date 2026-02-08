export interface Product {
    productId: number;
    name: string;
    description: string;
    price: number;
    createdAt: string | Date;
    categoryId: number;
}

export interface ProductsFilterData {
    name: string;
    minPrice: string;
    maxPrice: string;
    categoryId: string;
}

export interface ProductsFilter {
    name?: string;
    minPrice?: number;
    maxPrice?: number;
    categoryId?: number;
}