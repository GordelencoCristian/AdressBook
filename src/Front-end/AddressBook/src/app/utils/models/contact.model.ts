import { Contact } from "../interfaces/contact.interface";

export class ContactClass implements Contact {
  id: number | undefined;
  firstName!: string | undefined;
  lastName!: string | undefined;
  phoneNumber!: number | undefined;
  email!: string | undefined;
  address!: string | undefined;

  constructor(contact?: Contact) {
    if (contact) {
      this.id = contact.id ?? undefined;
      this.firstName = contact.firstName ?? undefined;
      this.lastName = contact.lastName ?? undefined;
      this.phoneNumber = contact.phoneNumber ?? undefined;
      this.email = contact.email ?? undefined;
      this.address = contact.address ?? undefined;
    }
  }
}
