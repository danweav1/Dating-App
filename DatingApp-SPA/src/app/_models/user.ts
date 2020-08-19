import { Photo } from './photo';

export interface User {
  id: number;
  username: string;
  knownAs: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: any; // changed to any to fix an issue with the html loading the date as a date but expecting a string
  photoUrl: string;
  city: string;
  country: string;
  interests?: string;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
