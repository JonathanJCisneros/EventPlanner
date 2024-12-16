import type { Errors, ValidationResponse } from './types.ts';

const isEmpty = (value: string): boolean => !value || /^\s$/g.test(value);

const formatWord = (word: string): string => {
    word = word.trim();

    if (word.length === 1) {
        word = word.toUpperCase();
    }
    else {
        word = word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
    }

    return word;
}

interface Validations {
    firstName(name: string, errors: Errors): ValidationResponse,
    lastName(name: string, errors: Errors): ValidationResponse,
    name(name: string, errors: Errors): ValidationResponse,
    email(email: string, errors: Errors): ValidationResponse,
    password(password: string, errors: Errors): ValidationResponse,
    confirmPassword(password: string, confirmPassword: string, errors: Errors): ValidationResponse,
    subject(subject: number, errors: Errors): ValidationResponse,
    message(message: string, errors: Errors): ValidationResponse
}

const validations: Validations = {
    firstName(name: string, errors: Errors): ValidationResponse {
        if (isEmpty(name)) {
            errors['firstName'] = 'First Name is required';
        }
        else if (name.length < 2) {
            errors['firstName'] = 'First Name must be 3 characters long';
        }
        else {
            name = formatWord(name);
        }

        return {
            value: name,
            errors: errors
        };
    },
    lastName(name: string, errors: Errors): ValidationResponse {
        if (isEmpty(name)) {
            errors['lastName'] = 'Last Name is required';
        }
        else if (name.length < 2) {
            errors['lastName'] = 'Last Name must be 3 characters long';
        }
        else {
            name = formatWord(name);
        }

        return {
            value: name,
            errors: errors
        };
    },
    name(name: string, errors: Errors): ValidationResponse {
        if (isEmpty(name)) {
            errors['name'] = 'Name is required';
        }
        else if (name.length < 3) {
            errors['name'] = 'Name must be at least 3 characters';
        }
        else if (name.length > 30) {
            errors['name'] = 'Name can be no more than 30 characters long';
        }
        else {
            name = name.replace(/ +(?= )/g, '').trim();

            let split: Array<string> = name.split(' ');

            split = split.map((x: string): string => formatWord(x));

            name = split.join(' ');
        }

        return {
            value: name,
            errors: errors
        }
    },
    email(email: string, errors: Errors): ValidationResponse {
        if (isEmpty(email)) {
            errors['email'] = 'Email is required';
        }
        else if (!/(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/g.test(email)) {
            errors['email'] = 'Please enter a valid Email';
        }
        else {
            email = email.trim().toLowerCase();
        }

        return {
            value: email,
            errors: errors
        }
    },
    password(password: string, errors: Errors): ValidationResponse {
        if (isEmpty(password)) {
            errors['password'] = 'Password is required';
        }
        else if (password.length < 8) {
            errors['password'] = 'Password must be at least 8 characters long';
        }
        else if (/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/g.test(password)) {
            errors['password'] = 'Password must have at least one upper case character, one lowercase character, one number and one special character';
        }
        else {
            password = password.trim();
        }

        return {
            value: password,
            errors: errors
        }
    },
    confirmPassword(password: string, confirmPassword: string, errors: Errors): ValidationResponse {
        if (isEmpty(confirmPassword)) {
            errors['confirmPassword'] = 'Confirm Password is required';
        }
        else if (confirmPassword !== password) {
            errors['confirmPassword'] = 'Passwords do not match';
        }

        return {
            value: confirmPassword,
            errors: errors
        }
    },
    subject(subject: number, errors: Errors): ValidationResponse {
        if (subject < 0) {
            errors['subject'] = 'Subject is required';
        }

        return {
            value: subject,
            errors: errors
        }
    },
    message(message: string, errors: Errors): ValidationResponse {
        if (isEmpty(message)) {
            errors['message'] = 'Message is required';
        }
        else if (message.length < 5) {
            errors['message'] = 'Message must be at least 5 characters';
        }
        else if (message.length > 350) {
            errors['message'] = 'Message must be no more than 350 characters long';
        }
        else {
            const formatted: string = message.replace(/ +(?= )/g, '').trim();

            message = formatted.charAt(0).toUpperCase() + formatted.slice(1).toLowerCase();
        }

        return {
            value: message,
            errors: errors
        }
    }    
}

export default validations;
