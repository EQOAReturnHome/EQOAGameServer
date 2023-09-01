function event_say()
diagOptions = {}
    npcDialogue = "I have been assigned the task of binding the spirits of those who wish to this place. Be mindful that you will return here upon an untimely death."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end