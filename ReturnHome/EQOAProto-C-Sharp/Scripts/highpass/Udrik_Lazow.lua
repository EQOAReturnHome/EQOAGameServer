function  event_say(choice)
diagOptions = {}
    npcDialogue = "Be careful not to get caught off guard by a picklaw.  If you run into them, do us all a service and kill them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end