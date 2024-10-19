export class Login{
  userName: string;
  password: string;

  constructor(userName?: string, password?: string ){
    if(userName && password){
      this.userName = userName;
      this.password = password;
    } else {
      this.userName = "";
      this.password = "";
    }

  }
}
