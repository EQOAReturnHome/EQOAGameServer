using System;
using EQOAUtilities;
using EQOAClientSpace;
using MessageSpace;
using EQOAAccountMessageTypes;
using System.Text;
using System.Threading.Tasks;

namespace EQOAAccount
{

    static class EQOAAccountManager
    {

    }

    public class AccountManagement
    {
        static async public void LoginRequest(EQOAClient ThisClient)
        {
            //Get first 2 integers... not sure on their purpose.
            int Unknown1 = Utilities.ReturnInt(ThisClient.ClientPacket);
            int Unknown2 = Utilities.ReturnInt(ThisClient.ClientPacket);

            //Get username
            //Appears 32 bytes is allocated for username
            ThisClient.Username = Utilities.ReturnString(ThisClient.ClientPacket, 32);

            Console.WriteLine($"Username: {ThisClient.Username}");

            //Get Encrypted Password
            //Appears 32 bytes is allocated for encrypted Password
            ThisClient.EncPassword = Utilities.ReturnString(ThisClient.ClientPacket, 32);

            Console.WriteLine($"Password: {ThisClient.EncPassword}");

            int unknown3 = Utilities.ReturnInt(ThisClient.ClientPacket);

            //Assuming next 60 bytes are saved for other game types? Unsure but works for now.
            string GameID = Utilities.ReturnString(ThisClient.ClientPacket, 60);


            //Do some stuff to verify username and password against database

            ThisClient.AccountVerified = true;

            if(ThisClient.AccountVerified == true)
            {
                //make packet and send to client for success on authentication

                //Pretend we are doing work here
                await Task.Delay(1000);

                new GoodLogin(ThisClient);
            }

            else
            {
                //Pretend we are doing work here
                await Task.Delay(1000);

                //Client failed to authenticate
                new BadAttempt(ThisClient);
            }
        }

        static async public void CreateAccount(EQOAClient ThisClient)
        {
            int Header = Utilities.ReturnInt(ThisClient.ClientPacket); //Message header
            ThisClient.Username = Utilities.ReturnString(ThisClient.ClientPacket, 32); // Username
            ThisClient.EncPassword = Utilities.ReturnString(ThisClient.ClientPacket, 32); //Encrypted Password
            //More information to get here possibly... concerns on privacy also?
            //Name, country, zip birthday
            //email may be good to gather

            Console.WriteLine($" Creating account using Username: {ThisClient.Username}");
            Console.WriteLine($"Encrypted Password: {ThisClient.EncPassword}");

            //Check database for username
            //If it exists
            //new BadAttempt(ThisClient);

            //Pretend we are doing work here
            await Task.Delay(1000);

            //If username is free, start saving to database
            //Should we save connecting ip address and time stamp the login?
            new GoodCreateAccount(ThisClient);
        }

        //This happens once you have logged in already, username should be saved to this Client Session
        static async public void ChangePassword(EQOAClient ThisClient)
        {
            Console.WriteLine($"{ThisClient.Username} is attempting to change password....");

            int Unknown1 = Utilities.ReturnInt(ThisClient.ClientPacket);
            int Unknown2 = Utilities.ReturnInt(ThisClient.ClientPacket);

            // Need to verify old password was correct against database
            string OldPassword = Utilities.ReturnString(ThisClient.ClientPacket, 32); //Encrypted Old Password
            
            //If old Password checks out against database, replace it with new password
            string NewPassword = Utilities.ReturnString(ThisClient.ClientPacket, 32); //Encrypted New Password

            Console.WriteLine($"Attempting to change Client Password. Old encrypted Password: {OldPassword}, New Encrypted Password: {NewPassword}");

            //Pretend we are doing work here
            await Task.Delay(1000);

            //Success
            new GoodChangePassword(ThisClient);

            //If Oldpassword verification fails, send failure
            //new BadAttempt(ThisClient);
        }
    }
}