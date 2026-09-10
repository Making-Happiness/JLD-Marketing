import type { IncomingMessage, ServerResponse } from 'node:http';

export interface HandleApiOptions {
  origin?: string;
  allowedOrigins?: Set<string>;
  secure?: boolean;
  dev?: boolean;
}

export declare function handleApi(
  req: IncomingMessage,
  res: ServerResponse,
  options?: HandleApiOptions
): Promise<boolean>;

