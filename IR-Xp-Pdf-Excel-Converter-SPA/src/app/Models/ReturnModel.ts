export class ReturnModel{
    success: boolean
    message: string
    downloadLinks: string[] = [];

    constructor() {
        this.success = false;
        this.message = "";
    }
}