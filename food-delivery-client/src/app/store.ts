import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../features/auth/authSlice";
import storesReducer from "../features/stores/storesSlice";
import productsReducer from "../features/products/productsSlice";
import cartReducer from "../features/cart/cartSlice";
import ordersReducer from "../features/orders/ordersSlice";
import partnersReducer from "../features/partners/partnersSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    stores: storesReducer,
    products: productsReducer,
    cart: cartReducer,
    orders: ordersReducer,
    partners: partnersReducer,
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
