import { create } from 'zustand'
import { IStore } from './interfaces/IStore';

export { useShallow } from 'zustand/react/shallow'

export const useStore: UseBoundStore<StoreApi<IStore>> = create<IStore>((set) => ({
    auth: undefined,
    setAuth: (auth: IAuth | undefined): void => set((state: IStore) => ({ auth }))
}));
