import {Group} from './group';

export type User = {
    id: number,
    firstName: string,
    lestName: string,
    email: string,
    groups: Group[]
}
