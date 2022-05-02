export class User {
  id: string | undefined;
  userName: string | undefined;
  email: string | undefined;
  jwtToken?: string;
  refreshToken?: string;
  access_token?: any;
  userRoles: any = [];
}
