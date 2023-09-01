function event_say()
diagOptions = {}
    npcDialogue = "It pains me to see that you have survived this long, though the fact that you're using necromancy in the open gives me hope for our kind. Keep your dark work up and our designs for this world may yet come to fruition."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end