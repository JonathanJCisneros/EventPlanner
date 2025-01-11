export type FormResponse = {
    success: boolean,
    message: string
};

export type UserFormResponse = {
    success: boolean,
    token: null | string,
    expirationDays: null | number,
    message: string
};

export type Errors = {
    [key: string]: undefined | string,
    FirstName?: string,
    LastName?: string,
    Name?: string,
    Email?: string,
    PhoneNumber?: string,
    Password?: string,
    ConfirmPassword?: string,
    Subject?: string,
    Message?: string    
};

export type ValidationResponse<T> = {
    value: T,
    errors: Errors
};

export type ServerValidationErrors = {
    [key: string]: undefined | string[],
    FirstName?: string[],
    LastName?: string[],
    Name?: string[],
    Email?: string[],
    PhoneNumber?: string[],
    Password?: string[],
    ConfirmPassword?: string[],
    Subject?: string[],
    Message?: string[]
};

export type ServerValidationResponse = {
    errors: ServerValidationErrors,
    status: number,
    title: string,
    traceId: string,
    type: string
}
