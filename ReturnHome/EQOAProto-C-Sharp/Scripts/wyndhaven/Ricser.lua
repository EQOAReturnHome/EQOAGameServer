function  event_say(choice)
diagOptions = {}
    npcDialogue = "I am on errand for my master, Bardif. Many a message is passed from here to Murnf."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end