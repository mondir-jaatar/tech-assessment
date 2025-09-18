import {PagedResponse} from "../../utilities/model/Response.tsx";

export interface GetSessionsFromPublicListingPageQuery {
    TargetAudience?: TargetAudience;
    DeliveryMode?: DeliveryMode;
    StartDate?: Date;
    EndDate?: Date;
    PageNumber: number;
    PageSize: number;
}

interface SessionFromPublicListingPageDto {
    
}

export interface GetSessionsFromPublicListingPageViewModelResponse extends PagedResponse<SessionFromPublicListingPageDto> {
    count: number;
}