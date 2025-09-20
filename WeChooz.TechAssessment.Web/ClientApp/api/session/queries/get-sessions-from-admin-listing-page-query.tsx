import {TargetAudience} from "../../enums/target-audience.tsx";
import {DeliveryMode} from "../../enums/delivery-mode.tsx";

export interface GetSessionsFromAdminListingPageQuery {
}

export interface SessionFromAdminListingPageDto {
    id: string;
    startDate: Date;
    course: CourseFromAdminListingPageDto;
    deliveryMode: DeliveryMode;
    duration: number;
    remainingSeats: number;
    trainer: TrainerFromAdminListingPageDto;
    numberOfParticipants: number;
}

export interface CourseFromAdminListingPageDto {
    id: string;
    name: string;
    description: CourseDescriptionFromAdminListingPageDto;
    targetAudience: TargetAudience;
    maxParticipants: number;
}

interface CourseDescriptionFromAdminListingPageDto {
    short: string;
    long: string;
}

interface TrainerFromAdminListingPageDto {
    id: string;
    firstName: string;
    lastName: string;
}