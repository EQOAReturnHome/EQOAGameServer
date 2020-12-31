using System;
using System.Text;
using EQOAAccountMessageTypes;
using EQOAClientSpace;
using EQOATCPServer;
using EQOAUtilities;

namespace MessageSpace
{
	public class BaseBuildMessage
	{
		public void PreparePacket(EQOAClient ThisClient)
		{

		}

		public void SendMessage(EQOAClient ThisClient)
        {
			//Get Packet Length, need to include the 4 bytes for the length also
			int Length = ThisClient.ResponsePacket.Count + 4;

			//Add to beginning of List
			ThisClient.ResponsePacket.InsertRange(0, Utilities.ReturnByteArray(Length));

			AsynchronousSocketListener.Send(ThisClient.Handler, ThisClient.ResponsePacket.ToArray());

			//Clear messages
			ThisClient.ClearMessages();

		}
	}

	public class GoodLogin : BaseBuildMessage
	{
        public GoodLogin(EQOAClient ThisClient)
        {
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(AccountMessageTypes.LOGIN_RESPONSE)); //LoginResponse
			ThisClient.ResponsePacket.AddRange(new byte[72]); //72 bytes of padding
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(ThisClient.AccountID)); //AccountID 4 bytes
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(ThisClient.Result)); //Result 2 bytes
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(ThisClient.AccountStatus)); //acct status 2 bytes
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(ThisClient.Subtime)); //subtime 4 bytes
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(ThisClient.Partime)); //partime 4 bytes
			ThisClient.ResponsePacket.AddRange(new byte[64]); //64 bytes of padding
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(ThisClient.Subfeatures));//subfeatures 4 bytes
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(ThisClient.Gamefeatures));//gamefeatures 4 bytes
			SendMessage(ThisClient);
        }
	}

	public class BadAttempt : BaseBuildMessage
	{
		public BadAttempt(EQOAClient ThisClient)
		{
            string Response;
            switch (ThisClient.MessageType)
            {
				case AccountMessageTypes.LOGIN_RESPONSE:
					Response = "Username or Password was incorrect.";
					break;

				case AccountMessageTypes.SUBMIT_ACCT_CREATE:
					Response = "An error occured or username is taken. Please try again and if unsuccessful, try another username. Thank you.";
					break;

				case AccountMessageTypes.CHANGE_PASSWORD:
					Response = "An error occured. Please try again. If this persists try a new password.";
					break;

				case AccountMessageTypes.DISABLED_FEATURE:
					Response = "This option is disabled in order to bring you content faster. If you feel this is an error,please contact the developer team for further assistance.";
					break;

				default:
					Response = "You've managed to do something wrong! Please let the developer team know you received this message.";
					break;

			}

			int StringLength = Response.Length;

			//Response Code
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(AccountMessageTypes.ACCT_CREATE_RESPONSE)); //LoginResponse
			for(int i = 0; i < 4; i++)
            {
				ThisClient.ResponsePacket.AddRange(new byte[4]);
			}

			//Add Our message
			ThisClient.ResponsePacket.AddRange(Encoding.ASCII.GetBytes(Response));

			//256 bytes reserved, Add padding for remaining bytes
			ThisClient.ResponsePacket.AddRange(new byte[256 - StringLength]);

			//Process Last bit of packet and send
			SendMessage(ThisClient);
		}
	}

	public class GoodChangePassword : BaseBuildMessage
	{
		public GoodChangePassword(EQOAClient ThisClient)
		{
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(AccountMessageTypes.CHANGE_PASSWORD_RESPONSE)); //Good Password Response
			ThisClient.ResponsePacket.AddRange(new byte[4]); //4 bytes of padding?
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray((int)1)); //1 is success I believe? 0 is probably failure? Been awhile
			ThisClient.ResponsePacket.AddRange(new byte[256]); //256 bytes here, assuming a message can go here? Untested

			SendMessage(ThisClient);
		}
	}

	public class GoodCreateAccount : BaseBuildMessage
	{
		public GoodCreateAccount(EQOAClient ThisClient)
		{
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray(AccountMessageTypes.ACCT_CREATE_RESPONSE)); //Good Password Response
			ThisClient.ResponsePacket.AddRange(new byte[4]); //4 bytes of padding?
			ThisClient.ResponsePacket.AddRange(Utilities.ReturnByteArray((int)1)); //1 is success I believe? 0 is probably failure? Been awhile
			ThisClient.ResponsePacket.AddRange(new byte[4]); //4 bytes of padding?
			ThisClient.ResponsePacket.AddRange(new byte[256]); //256 bytes here, assuming a message can go here? Untested

			SendMessage(ThisClient);
		}
	}
}
