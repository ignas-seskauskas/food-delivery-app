import { OrderStatus } from "./enums";
import { ProductResponseDto, ProductState } from "./product";
import { StoreResponseDto } from "./store";

export interface OrderItemRequestDto {
  productId: number;
  quantity: number;
}

export interface OrderItemResponseDto {
  productId: number;
  orderId: number;
  quantity: number;
  totalPrice: number;
  product: ProductResponseDto;
}

export interface OrderRequestDto {
  storeId: number;
  items: OrderItemRequestDto[];
}

export interface OrderResponseDto {
  id: number;
  customerId: number;
  storeId: number;
  store: StoreResponseDto;
  createdAt: Date;
  itemsPrice: number;
  deliveryFee: number;
  totalPrice: number;
  orderStatus: OrderStatus;
  items: OrderItemResponseDto[];
}

export interface OrderItemState {
  productId: number;
  orderId: number;
  quantity: number;
  totalPrice: number;
  product: ProductState;
}

export interface OrderState {
  id: number;
  customerId: number;
  storeId: number;
  store: StoreResponseDto;
  createdAt: Date;
  itemsPrice: number;
  deliveryFee: number;
  totalPrice: number;
  orderStatus: OrderStatus;
  items: OrderItemState[];
}