export interface User {
    id : string; 
    userId : string; 
    userName : string; 
    email ?: string; 
    birthDate : Date; 
    created ?: Date; 
    updated ?: Date; 
    gender ?: string; 
    phone ?: string; 
}

export interface SearchResult<T> {
    total: number;
    data: T[]
}