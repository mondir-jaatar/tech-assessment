import {PagedResponse} from "../../utilities/model/Response.tsx";
import {DeliveryMode} from "../../enums/delivery-mode.tsx";
import {TargetAudience} from "../../enums/target-audience.tsx";

export interface GetSessionsFromPublicListingPageQuery {
    TargetAudience?: TargetAudience;
    DeliveryMode?: DeliveryMode;
    StartDate?: Date;
    EndDate?: Date;
    PageNumber: number;
    PageSize: number;
}

export interface SessionFromPublicListingPageDto {
    id: string;
    startDate: Date;
    course: CourseFromPublicListingPageDto;
    deliveryMode: DeliveryMode;
    duration: number;
    remainingSeats: number;
    trainer: TrainerFromPublicListingPageDto;
    numberOfParticipants: number;
}

export interface CourseFromPublicListingPageDto {
    id: string;
    name: string;
    description: CourseDescriptionFromPublicListingPageDto;
    targetAudience: TargetAudience;
    maxParticipants: number;
}

interface CourseDescriptionFromPublicListingPageDto {
    short: string;
    long: string;
}

interface TrainerFromPublicListingPageDto {
    firstName: string;
    lastName: string;
}

export interface GetSessionsFromPublicListingPageViewModelResponse extends PagedResponse<SessionFromPublicListingPageDto> {
    count: number;
}