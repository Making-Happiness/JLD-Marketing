import { createContext, useContext } from 'react';
export interface SessionUser { id: number; email: string }
export const SessionContext = createContext<SessionUser | null>(null);
export const useSession = () => useContext(SessionContext);
