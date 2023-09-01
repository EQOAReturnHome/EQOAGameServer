function event_say()
diagOptions = {}
    npcDialogue = "Here at the Emerald Lodge we strive to guide our rangers through a life changing journey. Hopefully they will use this experience to become wise in the ways of the world. May they help guide our kind to unite with nature, and eventually return to the Goddess Tunare."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end