import {SessionCommandBase} from "./session-command-base.tsx";

export interface UpdateSessionCommand extends SessionCommandBase
{
    id: string;
    rowVersion: string;
}