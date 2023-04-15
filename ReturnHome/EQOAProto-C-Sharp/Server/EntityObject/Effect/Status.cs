using System;

namespace ReturnHome.Server.EntityObject.Effect
{
    public unsafe struct Status
    {
        private const int MAX_SIZE = 62;

        private readonly uint _resourceId;
        private readonly int _size;
        private fixed char _name[MAX_SIZE];

        public Status(uint resourceId, string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            int size = name.Length;

            _resourceId = resourceId;
            _size = size <= MAX_SIZE ? size : MAX_SIZE;

            fixed (char* pName = &_name[0])
            {
                name[.._size].CopyTo(new Span<char>(pName, MAX_SIZE));
            }
        }
    }
}
