import { PaginationModel } from "../interfaces/pagination.interface";

export class PaginationClass implements PaginationModel {
	currentPage: number = 1;
	pageSize: number = 10;
	totalCount: number = 100;
	totalPages: number = 10;
}
