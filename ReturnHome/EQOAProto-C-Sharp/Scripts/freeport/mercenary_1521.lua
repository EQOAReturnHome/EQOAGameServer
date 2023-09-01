function  event_say(choice)
diagOptions = {}
    npcDialogue = "Order! We will have order! Keep your eyes, peeled and no sleeping on the job!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end