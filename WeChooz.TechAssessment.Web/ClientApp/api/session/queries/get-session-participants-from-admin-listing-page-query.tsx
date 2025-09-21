export interface GetSessionParticipantsFromAdminPageQuery
{
    sessionId: string;
}

export interface SessionParticipantFromSessionAdminDto
{
    id?: string;
    firstName: string;
    lastName: string;
    email: string;
    companyName: string;
}