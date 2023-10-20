import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject, debounceTime, delay, switchMap, tap } from "rxjs";
import { LoaderService } from "src/shared/loader.service";
import { SearchResult, User } from "src/shared/models/user";
import { UsersService } from "src/shared/users.service";

interface State {
    page: number;
    pageSize: number;
    userId: string;
    userName: string;
    phone: string;
}


@Injectable({
    providedIn: 'root',
})
export class TableService {
    private _state: State = {
        page: 1,
        pageSize: 10,
        phone: '',
        userId: '',
        userName: '',
    };


    private _search$ = new Subject<void>();
    private _data$ = new BehaviorSubject<User[]>([]);
    private _total$ = new BehaviorSubject<number>(0);

    constructor(private service: UsersService,
        private loader: LoaderService) {
        this._search$
            .pipe(
                tap(() => this.loader.setLoading(true)),
                debounceTime(200),
                switchMap(() => this._search()),
                delay(200),
            )
            .subscribe((result) => {
                this._data$.next(result.data);
                this._total$.next(result.total);
                this.loader.setLoading(false)
            });

        this._search$.next();
    }

    get data$() {
        return this._data$.asObservable();
    }
    get total$() {
        return this._total$.asObservable();
    }

    get page() {
        return this._state.page;
    }
    get pageSize() {
        return this._state.pageSize;
    }
    get phone() {
        return this._state.phone;
    }

    get userId() {
        return this._state.userId;
    }

    get userName() {
        return this._state.userName;
    }

    set page(page: number) {
        this._set({ page });
    }
    set pageSize(pageSize: number) {
        this._set({ pageSize });
    }
    set userId(userId: string) {
        this._set({ userId });
    }

    set userName(userName: string) {
        this._set({ userName });
    }

    set phone(phone: string) {
        this._set({ phone });
    }


    private _set(patch: Partial<State>) {
        Object.assign(this._state, patch);
        this._search$.next();
    }

    public refresh(){
        this._search$.next();
    }

    private _search(): Observable<SearchResult<User>> {
        const { pageSize, page, userId, userName, phone } = this._state;
        return this.service.get(userName, phone, userId, pageSize, page);
    }
}