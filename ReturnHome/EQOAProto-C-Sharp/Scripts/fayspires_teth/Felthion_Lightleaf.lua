function event_say()
diagOptions = {}
    npcDialogue = "Though Tethelin gives small hope, our people will soon vanish from Tunaria as the Elddar did. Until that day, we will carry the song of the forest to all that would hear it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end