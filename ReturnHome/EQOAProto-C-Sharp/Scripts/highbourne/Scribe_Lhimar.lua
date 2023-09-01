function event_say()
diagOptions = {}
    npcDialogue = "Translating and documenting. It is a simple life but I enjoy the excitement these pages bring all while safely within the walls of the Library Archive."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end