function event_say()
diagOptions = {}
    npcDialogue = "The Senate and this paladin are driving me to my limits. I need more information on how this undead came to be. The torn document doesn't make any sense and I have yet to find any source to translate it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end