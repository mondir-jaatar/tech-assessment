import {DeliveryMode} from "../../enums/delivery-mode.tsx";

export interface SessionCommandBase {
    startDate: Date;
    deliveryMode: DeliveryMode;
    duration: number;
    courseId: string;
}