import { Component } from '@angular/core';
import { globalModules } from '../../app.config';
import { ContactService } from '../../utils/services/contact.service';
import { Contact } from '../../utils/interfaces/contact.interface';
import { FormsModule } from '@angular/forms';
import { PaginationClass } from '../../utils/models/pagination.model';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent,
} from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../../utils/shared/dialog/dialog.component';

@Component({
  selector: 'app-contacts',
  standalone: true,
  imports: [
    ...globalModules,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatPaginator,
    MatProgressBarModule,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.scss',
})
export class ContactsComponent {

  constructor(private contactService: ContactService,
    public dialog: MatDialog
  ) {}

  loading: boolean = false;
  pagedSummary: PaginationClass = new PaginationClass();

  dataSource!: MatTableDataSource<Contact>;
  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'phoneNumber',
    'email',
    'address',
    'actions',
  ];

  ngOnInit() {
    this.list();
  }

  pageChanged(event: PageEvent) {
    this.pagedSummary.pageSize = event.pageSize;
    this.pagedSummary.currentPage = event.pageIndex + 1;

    this.list();
  }

  list() {
    this.loading = true;

    const request = {
      page: this.pagedSummary?.currentPage ?? 1,
      itemsPerPage: this.pagedSummary?.pageSize ?? 10,
    };

    this.contactService.list(request).subscribe((res) => {
      if (res) {
        this.dataSource = new MatTableDataSource<Contact>(res.items);
        this.pagedSummary = res.pagedSummary;
      }
    });

    this.loading = false;
  }

  deleteContact(contact: Contact){
    if(contact.id){
      this.contactService.delete(contact.id).subscribe( () => {
        this.list()
    })
    }
  }

  openDialog(contact: Contact): void {
    this.dialog.open(DialogComponent, {
      data: contact,
    });
  }
}
