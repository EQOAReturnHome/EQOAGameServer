function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to the Chosen of Gunthak. If you are willing to put down your tools of death, and listen to the spirits, you may learn something here."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end