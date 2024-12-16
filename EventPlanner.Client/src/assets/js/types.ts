export type FormResponse = {
    success: boolean,
    message: string
}

export type Errors = {
    firstName?: string,
    lastName?: string,
    name?: string,
    email?: string,
    password?: string,
    confirmPassword?: string,
    birthday?: string,
    subject?: string,
    message?: string
}

export type ValidationResponse = {
    value: string | number,
    errors: Errors
}
