function  event_say(choice)
diagOptions = {}
    npcDialogue = "If a warrior requires discipline, they will fight here. Honor may be regained here in the Keeps' Arena."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end