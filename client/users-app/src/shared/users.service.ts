import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environment";
import { SearchResult, User } from "./models/user";
import { Observable } from "rxjs";



@Injectable({
    providedIn: 'root',
})
export class UsersService {
    private serverUrl: string;
	
    constructor(private httpClient: HttpClient) {
        this.serverUrl = environment.serverUrl;
    }

    public get(userName: string, phone: string, userId: string, limit: number, offset: number): Observable<SearchResult<User>> {
        return this.httpClient.get<SearchResult<User>>(`${this.serverUrl}/Users`, { params: { userName, phone, userId, limit, offset } });
    }

    public getById(id: string): Observable<User> {
        return this.httpClient.get<User>(`${this.serverUrl}/Users/${id}`);
    }

    public delete(id: string) {
        return this.httpClient.delete(`${this.serverUrl}/Users/${id}`);
    }

    public create(model: User) {
        return this.httpClient.post<User>(`${this.serverUrl}/Users`, model);
    }

    public update(model: User) {
        return this.httpClient.put<User>(`${this.serverUrl}/Users`, model);
    }
}