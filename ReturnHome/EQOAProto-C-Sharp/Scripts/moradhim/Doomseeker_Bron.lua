function  event_say(choice)
diagOptions = {}
    npcDialogue = "Pardon me, I must keep a sharp eye out for those that would do harm ta this city and ta the Children of Below."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end