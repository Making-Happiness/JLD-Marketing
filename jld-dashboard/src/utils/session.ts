import { createContext, useContext } from 'react';
export interface SessionUser {
  id: string; email: string }
export const SessionContext = createContext<SessionUser | null>(null);
export const useSession = () => useContext(SessionContext);
