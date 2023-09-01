function  event_say(choice)
diagOptions = {}
    npcDialogue = "Have you rotting maggots for eyes?! Do you not see that I am occupied?! Return to the main hall immediately!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end